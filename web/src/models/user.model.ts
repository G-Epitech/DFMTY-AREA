export class UserModel {
  readonly #id: string;
  readonly #email: string;
  readonly #firstName: string;
  readonly #lastName: string;
  readonly #picture: string;

  constructor(
    id: string,
    email: string,
    firstName: string,
    lastName: string,
    picture: string
  ) {
    this.#id = id;
    this.#email = email;
    this.#firstName = firstName;
    this.#lastName = lastName;
    this.#picture = picture;
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

  get picture(): string {
    return this.#picture;
  }
}
