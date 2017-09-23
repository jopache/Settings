FROM alpine:3.5 AS base
RUN apk add --no-cache nodejs-current tini && rm -rf /var/cache/apk/*
WORKDIR /app
ENTRYPOINT ["/sbin/tini", "--"]
COPY src/Settings.Web/package.json .

FROM base AS dependencies
RUN npm set progress=false && npm config set depth 0
RUN npm install --only=production
RUN cp -R node_modules prod_node_modules
RUN npm install

FROM dependencies as build
COPY src/Settings.Web/ .
RUN  npm run lint
RUN npm run build:local

FROM nginx:alpine as release
EXPOSE 80
COPY --from=build /app/dist /usr/share/nginx/html
