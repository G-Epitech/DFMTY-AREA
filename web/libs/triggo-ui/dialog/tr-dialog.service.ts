import type { ComponentType } from '@angular/cdk/portal';
import { Injectable, type TemplateRef, inject } from '@angular/core';
import {
  type BrnDialogOptions,
  BrnDialogService,
  DEFAULT_BRN_DIALOG_OPTIONS,
  cssClassesToArray,
} from '@spartan-ng/ui-dialog-brain';
import { TrDialogContentComponent } from './tr-dialog-content.component';
import { trDialogOverlayClass } from './tr-dialog-overlay.directive';

// eslint-disable-next-line @typescript-eslint/no-explicit-any
export type TrDialogOptions<DialogContext = any> = BrnDialogOptions & {
  contentClass?: string;
  context?: DialogContext;
};

@Injectable({
  providedIn: 'root',
})
export class TrDialogService {
  private readonly _brnDialogService = inject(BrnDialogService);

  public open(
    component: ComponentType<unknown> | TemplateRef<unknown>,
    options?: Partial<TrDialogOptions>
  ) {
    options = {
      ...DEFAULT_BRN_DIALOG_OPTIONS,
      closeDelay: 100,
      // eslint-disable-next-line
      ...(options ?? {}),
      backdropClass: cssClassesToArray(
        `${trDialogOverlayClass} ${options?.backdropClass ?? ''}`
      ),
      context: {
        ...options?.context,
        $component: component,
        $dynamicComponentClass: options?.contentClass,
      },
    };

    return this._brnDialogService.open(
      TrDialogContentComponent,
      undefined,
      options.context,
      options
    );
  }
}
