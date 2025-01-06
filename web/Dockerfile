FROM node:alpine

RUN npm install -g @angular/cli

RUN npm install -g pnpm

WORKDIR /usr/src/app

COPY package.json .
COPY pnpm-lock.yaml .

RUN pnpm install

COPY . .

CMD ["ng", "serve", "--host", "0.0.0.0"]
