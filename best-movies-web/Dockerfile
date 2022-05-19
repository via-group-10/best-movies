FROM node:16.4.2 AS build
EXPOSE 8080
LABEL name=best-movies-web

WORKDIR /app

COPY package.json ./
COPY package-lock.json ./
RUN npm install
COPY . ./
RUN npm run build

FROM nginx
EXPOSE 8080
COPY --from=build /app/public /usr/share/nginx/html
