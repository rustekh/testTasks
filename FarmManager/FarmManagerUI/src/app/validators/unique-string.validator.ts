import { FormControl } from "@angular/forms";
import * as _ from 'lodash';

export function uniqueStringValidator(existingValues: string[]): ((control: FormControl) => { [key: string]: any } | null) {
    return (control: FormControl): { [key: string]: any } | null => {
        if (!_.isEmpty(control.value)
            && _.find(existingValues,
                ev => !_.isEmpty(ev) && ev.trim().toLowerCase() === control.value.trim().toLowerCase())) {
            return { notunique: true };
        }
        return null;
    }
}