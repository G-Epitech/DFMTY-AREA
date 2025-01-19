import { ChangeDetectionStrategy, Component } from '@angular/core';
import { TrButtonDirective } from '@triggo-ui/button';
import { NgIcon } from '@ng-icons/core';

interface FAQItem {
  question: string;
  answer: string;
  isOpen: boolean;
  links?: string[];
}

@Component({
  selector: 'tr-faq',
  imports: [TrButtonDirective, NgIcon],
  templateUrl: './faq.page.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
})
export class FaqPageComponent {
  faqItems: FAQItem[] = [
    {
      question: 'How can I create an automation?',
      answer:
        "To create an automation, navigate to the 'Automations' section in your side menu once you sign up. There you can go into the automation workspace. Remember you need at least one trigger, and all parameters to not be empty.",
      isOpen: false,
    },
    {
      question: 'How to link an integration?',
      answer:
        'You can go to the integrations listing page, there you have a button with link integration. Choose your integration and you can link it to your account for a platform. These integrations will be useful for creating automations',
      isOpen: false,
    },
    {
      question: 'How to link OpenAI?',
      answer: 'To link OpenAI you will need a OpenAI API Key and a Admin Key.',
      links: [
        'https://platform.openai.com/settings/organization/admin-keys',
        'https://platform.openai.com/api-keys',
      ],
      isOpen: false,
    },
    {
      question: 'How to link League of Legends account?',
      answer:
        'To link a league of legends account you can check out the Summoner name as well as the Tag line. You can find these details on op.gg',
      links: ['https://www.op.gg/'],
      isOpen: false,
    },
  ];

  toggleFAQ(item: FAQItem): void {
    item.isOpen = !item.isOpen;
  }
}
