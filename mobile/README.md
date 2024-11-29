# triggo

Triggo's Android and iOS applications

## Getting Started

Triggo is a mobile application that allows users to create and manage their own tasks and reminders.
The application is available on both Android and iOS platforms.

## Contributing

Feel free to contribute to the project by creating a pull request.

## Running the application

First, you need to create a `.env` file in the root directory of the project. If you want, you can use the `.env.example` file as a template.
To run the application locally, you can run the following commands:

```bash
flutter pub get
flutter run --dart-define-from-file=.env
```

## Running the tests

And if you want to lunch the tests locally, you can run the following commands:

```bash
flutter pub run build_runner build 
flutter test
```
