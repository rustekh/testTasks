import { ErrorHandler, Injectable } from "@angular/core";

@Injectable()
export class ErrorInterceptor implements ErrorHandler {
    handleError(error: any): void {
        console.log('something went wrong');
    }
}