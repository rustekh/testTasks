import { Component, OnInit } from '@angular/core';
import { FormBuilder, ValidatorFn, Validators } from '@angular/forms';
import { Animal } from './models/animal.model';
import { AnimalsService } from './services/animals.service';
import { uniqueStringValidator } from './validators/unique-string.validator';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  private readonly formBuilder = new FormBuilder();

  public animals: Animal[] = [];
  public newAnimalForm = this.formBuilder.group({
    newAnimalName: ['']
  });

  constructor(private readonly animalsService: AnimalsService) { }

  public ngOnInit(): void {
    this.loadAnimals();
  }

  public async onAnimalNameKeyUp(event: KeyboardEvent): Promise<void> {
    if (event.key === "Enter") {
      await this.addAnimal();
    }
  }

  public async addAnimal(): Promise<void> {
    this.newAnimalForm.markAllAsTouched();

    if (this.newAnimalForm.valid) {
      const newAnimal = {
        name: this.newAnimalForm.value.newAnimalName.trim()
      } as Animal;

      await this.animalsService.add(newAnimal);
      this.newAnimalForm.controls['newAnimalName'].setValue('');
      await this.loadAnimals();
    }
  }

  public async deleteAnimal(animalName: string): Promise<void> {
    await this.animalsService.delete(animalName);
    await this.loadAnimals();
  }

  private async loadAnimals(): Promise<void> {
    this.animals = await this.animalsService.list();
    const existingAnimalNames = this.animals.map(a => a.name);
    this.newAnimalForm.controls['newAnimalName'].setValidators(
      [
        Validators.required,
        Validators.pattern(/[^\s]/),
        uniqueStringValidator(existingAnimalNames) as ValidatorFn
      ]);
    this.newAnimalForm.controls['newAnimalName'].updateValueAndValidity();
  }
}
