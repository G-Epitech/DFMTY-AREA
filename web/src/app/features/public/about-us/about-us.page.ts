import { ChangeDetectionStrategy, Component } from '@angular/core';
import { NgOptimizedImage } from '@angular/common';

interface TeamMember {
  name: string;
  titles: string[];
}

@Component({
  selector: 'tr-about-us',
  imports: [NgOptimizedImage],
  templateUrl: './about-us.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class AboutUsPageComponent {
  backendTeam: TeamMember[] = [
    { name: 'Matheo Coquet', titles: ['CTO', 'CEO', 'CPO', 'CMO'] },
    { name: 'Flavien Chenu', titles: ['C# Developper', 'Lead Back'] },
  ];

  frontendTeam: TeamMember[] = [
    {
      name: 'Yann Masson',
      titles: ['Flutter Developper', 'Angular Developper'],
    },
    { name: 'Thomas Boué', titles: ['Flutter Developper'] },
    { name: 'Suceveanu Dragos', titles: ['Angular Developper'] },
  ];
}
