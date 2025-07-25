FROM node:20-alpine

WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .
# Substitui o environment.ts pelo environment.docker.ts
RUN cp src/environments/environment.docker.ts src/environments/environment.ts

EXPOSE 4200

CMD ["npm", "start"] 