import { EditSheetService } from '@features/automations/workspace/components/edit-sheets/edit-sheet.service';
import { AutomationStepSelectionType } from '@features/automations/workspace/components/edit-sheets/edit-sheet.types';
import { inject } from '@angular/core';
import { SchemaStore } from '@app/store/schema-store';
import { AutomationWorkspaceStore } from '@features/automations/workspace/automation-workspace.store';
import { AutomationSchemaModel } from '@models/automation';

export class EditSheetComponentBase {
  protected readonly schemaStore = inject(SchemaStore);
  protected readonly workspaceStore = inject(AutomationWorkspaceStore);
  readonly #baseService: EditSheetService;

  schema: AutomationSchemaModel | null = null;
  protected readonly AutomationSelectionStepType = AutomationStepSelectionType;

  constructor(service: EditSheetService) {
    this.#baseService = service;
  }

  protected onBack(): void {
    this.#baseService.back();
  }
}
