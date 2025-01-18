import { Injectable } from '@angular/core';
import { EditSheetService } from '@features/automations/workspace/components/edit-sheets/edit-sheet.service';

@Injectable({
  providedIn: 'root',
})
export class SelectActionSheetService extends EditSheetService {
  constructor() {
    super();
  }
}
