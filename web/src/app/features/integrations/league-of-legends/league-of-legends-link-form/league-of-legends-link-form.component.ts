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
import {
  LeagueOfLegendsMediator,
  OpenaiMediator,
} from '@mediators/integrations';
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
  readonly #mediator = inject(LeagueOfLegendsMediator);
  readonly #toastr = inject(ToastrService);

  #destroyRef = new Subject<void>();

  leagueOfLegendsForm = new FormGroup({
    gameName: new FormControl(''),
    tagLine: new FormControl(''),
  });

  loading = signal<boolean>(false);

  ngOnDestroy() {
    this.#destroyRef.next();
    this.#destroyRef.complete();
  }

  link() {
    const gameName = this.leagueOfLegendsForm.controls.gameName.value;
    const tagLine = `#${this.leagueOfLegendsForm.controls.tagLine.value}`;

    if (!gameName || !tagLine) {
      this.#toastr.error('Game Name and Tag Line are required', 'Link failed');
      return;
    }
    this.loading.set(true);
    this.#mediator
      .link(gameName, tagLine)
      .pipe(
        takeUntil(this.#destroyRef),
        finalize(() => this.loading.set(false))
      )
      .subscribe({
        next: () => {
          this.#toastr.success('League of Legends account linked successfully');
        },
        error: () => {
          this.#toastr.error('Failed to link League of Legends account');
        },
      });
  }
}
