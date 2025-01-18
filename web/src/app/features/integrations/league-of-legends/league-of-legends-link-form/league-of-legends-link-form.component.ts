import {
  ChangeDetectionStrategy,
  Component,
  inject,
  OnDestroy,
  signal,
} from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { LabelDirective } from '@triggo-ui/label';
import { TrButtonDirective } from '@triggo-ui/button';
import { TrInputDirective } from '@triggo-ui/input';
import { TrSpinnerComponent } from '@triggo-ui/spinner';
import { OpenaiMediator } from '@mediators/integrations';
import { ToastrService } from 'ngx-toastr';
import { Subject, takeUntil } from 'rxjs';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'tr-league-of-legends-link-form',
  imports: [
    FormsModule,
    LabelDirective,
    TrButtonDirective,
    TrInputDirective,
    TrSpinnerComponent,
    ReactiveFormsModule,
  ],
  templateUrl: './league-of-legends-link-form.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class LeagueOfLegendsLinkFormComponent implements OnDestroy {
  readonly #openaiMediator = inject(OpenaiMediator);
  readonly #toastr = inject(ToastrService);

  #destroyRef = new Subject<void>();

  leagueOfLegendsForm = new FormGroup({
    gameName: new FormControl(''),
    tagName: new FormControl(''),
  });

  loading = signal<boolean>(false);

  ngOnDestroy() {
    this.#destroyRef.next();
    this.#destroyRef.complete();
  }

  link() {
    const apiToken = this.leagueOfLegendsForm.controls.gameName.value;
    const adminApiToken = this.leagueOfLegendsForm.controls.tagName.value;

    if (!apiToken || !adminApiToken) {
      this.#toastr.error(
        'API key and Admin API key are required',
        'Link failed'
      );
      return;
    }
    this.loading.set(true);
    this.#openaiMediator
      .link(apiToken, adminApiToken)
      .pipe(
        takeUntil(this.#destroyRef),
        finalize(() => this.loading.set(false))
      )
      .subscribe({
        next: () => {
          this.#toastr.success('OpenAI linked successfully');
        },
        error: () => {
          this.#toastr.error('Failed to link OpenAI');
        },
      });
  }
}
