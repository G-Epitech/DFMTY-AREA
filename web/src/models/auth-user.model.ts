export class AuthUserModel {
  readonly #id: string;
  readonly #email: string;
  readonly #firstName: string;
  readonly #lastName: string;

  constructor(id: string, email: string, firstName: string, lastName: string) {
    this.#id = id;
    this.#email = email;
    this.#firstName = firstName;
    this.#lastName = lastName;
  }

  get id(): string {
    return this.#id;
  }

  get email(): string {
    return this.#email;
  }

  get firstName(): string {
    return this.#firstName;
  }

  get lastName(): string {
    return this.#lastName;
  }
}
