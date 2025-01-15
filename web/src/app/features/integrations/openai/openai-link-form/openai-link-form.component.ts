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
import { TrInputDirective } from '@triggo-ui/input';
import { TrButtonDirective } from '@triggo-ui/button';
import { ToastrService } from 'ngx-toastr';
import { OpenaiMediator } from '@mediators/integrations';
import { Subject, takeUntil } from 'rxjs';
import { finalize } from 'rxjs/operators';
import { TrSpinnerComponent } from '@triggo-ui/spinner';

@Component({
  selector: 'tr-openai-link-form',
  imports: [
    FormsModule,
    LabelDirective,
    TrInputDirective,
    TrButtonDirective,
    ReactiveFormsModule,
    TrSpinnerComponent,
  ],
  templateUrl: './openai-link-form.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class OpenaiLinkFormComponent implements OnDestroy {
  readonly #openaiMediator = inject(OpenaiMediator);
  readonly #toastr = inject(ToastrService);

  #destroyRef = new Subject<void>();

  openaiForm = new FormGroup({
    apiToken: new FormControl(''),
    adminApiToken: new FormControl(''),
  });

  loading = signal<boolean>(false);

  ngOnDestroy() {
    this.#destroyRef.next();
    this.#destroyRef.complete();
  }

  link() {
    const apiToken = this.openaiForm.controls.apiToken.value;
    const adminApiToken = this.openaiForm.controls.adminApiToken.value;

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
