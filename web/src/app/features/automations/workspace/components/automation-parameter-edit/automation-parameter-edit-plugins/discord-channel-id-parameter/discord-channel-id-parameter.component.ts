import {
  ChangeDetectionStrategy,
  Component,
  effect,
  EventEmitter,
  inject,
  signal,
} from '@angular/core';
import {
  ParameterEditDynamicComponent,
  ParameterEditOutput,
} from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.types';
import { AutomationParameterValueType } from '@models/automation';
import { IntegrationsMediator } from '@mediators/integrations';
import { AutomationParameterEditService } from '@features/automations/workspace/components/automation-parameter-edit/automation-parameter-edit.service';
import { Observable } from 'rxjs';
import { DiscordChannelModel } from '@models/integration';
import { AsyncPipe, NgClass } from '@angular/common';
import { TrButtonDirective } from '@triggo-ui/button';
import { NgIcon } from '@ng-icons/core';

@Component({
  selector: 'tr-discord-channel-id-parameter',
  imports: [AsyncPipe, TrButtonDirective, NgClass, NgIcon],
  templateUrl: './discord-channel-id-parameter.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class DiscordChannelIdParameterComponent
  implements ParameterEditDynamicComponent
{
  parameter!: { identifier: string; value: string | null };
  parameterType!: AutomationParameterValueType;
  valueChange = new EventEmitter<ParameterEditOutput>();
  integrationId: string | undefined;

  readonly #integrationsMediator = inject(IntegrationsMediator);
  readonly #paramService = inject(AutomationParameterEditService);

  channels$: Observable<DiscordChannelModel[]> | undefined;
  guildSelected = signal<boolean>(false);

  constructor() {
    effect(() => {
      const guildId = this.#paramService
        .currentParameters()
        .find(({ identifier }) => identifier === 'GuildId')?.value;
      this.guildSelected.set(!!guildId);
      if (this.integrationId && guildId) {
        this.channels$ = this.#integrationsMediator.getDiscordChannels(
          this.integrationId,
          guildId
        );
      }
    });
  }

  selectChannel(channelId: string): void {
    this.valueChange.emit({ rawValue: channelId });
  }

  isChannelSelected(channelId: string): boolean {
    return this.parameter.value === channelId;
  }
}
