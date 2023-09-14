import { HttpClientModule } from '@angular/common/http';
import { ErrorHandler, NgModule } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { ErrorInterceptor } from './interceptors/error.interceptor';
import { AnimalsService } from './services/animals.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    MatFormFieldModule,
    MatInputModule,
    ReactiveFormsModule,
    BrowserAnimationsModule
  ],
  providers: [AnimalsService,
    {
      provide: ErrorHandler,
      useClass: ErrorInterceptor
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
