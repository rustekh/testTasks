import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { firstValueFrom } from "rxjs";
import { environment } from "src/environments/environment";
import { Animal } from "../models/animal.model";

@Injectable()
export class AnimalsService {
    private _baseUrl = `${environment.apiUrl}/animals`;

    constructor(private readonly httpClient: HttpClient) { }

    public async list(): Promise<Animal[]> {
        const res = await firstValueFrom(this.httpClient.get(this._baseUrl));
        return res as Animal[];
    }

    public async add(animal: Animal): Promise<void> {
        await firstValueFrom(this.httpClient.post(this._baseUrl, animal));
    }

    public async delete(animalName: string): Promise<void> {
        await firstValueFrom(this.httpClient.delete(`${this._baseUrl}/${animalName}`));
    }
}
