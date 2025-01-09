import {
  ChangeDetectionStrategy,
  Component,
  inject,
  input,
  OnInit,
} from '@angular/core';
import {
  BrnDialogImports,
  BrnDialogRef,
  injectBrnDialogContext,
} from '@spartan-ng/ui-dialog-brain';
import { AsyncPipe, NgClass, NgOptimizedImage } from '@angular/common';
import { TrDialogImports } from '@triggo-ui/dialog';
import { NgIcon } from '@ng-icons/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { TrInputSearchComponent } from '@triggo-ui/input';
import { IntegrationsMediator } from '@mediators/integrations.mediator';
import { Observable } from 'rxjs';
import { DiscordGuildModel } from '@models/integration';

@Component({
  selector: 'tr-manage-guild-dialog',
  imports: [
    NgOptimizedImage,
    TrDialogImports,
    BrnDialogImports,
    NgIcon,
    NgClass,
    TrButtonDirective,
    TrInputSearchComponent,
    AsyncPipe,
  ],
  templateUrl: './manage-guild-dialog.component.html',
  standalone: true,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ManageGuildDialogComponent implements OnInit {
  readonly #integrationMediator = inject(IntegrationsMediator);
  readonly #dialogContext = injectBrnDialogContext<{
    integrationId: string;
  }>();

  protected readonly integrationId = this.#dialogContext.integrationId;

  guilds$: Observable<DiscordGuildModel[]> | undefined;

  ngOnInit() {
    if (this.integrationId) {
      this.guilds$ = this.#integrationMediator.getDiscordGuilds(this.integrationId);
    }
  }
}
