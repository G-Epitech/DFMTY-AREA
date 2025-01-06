FROM mobile-base

WORKDIR /app

COPY . .

RUN flutter pub get

RUN flutter analyze

CMD ["flutter", "test"]
