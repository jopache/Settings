FROM alpine:3.5 AS base
RUN apk add --no-cache nodejs-current tini
WORKDIR /app
ENTRYPOINT ["/sbin/tini", "--"]
COPY src/Settings.Web/package.json .

FROM base AS dependencies
RUN npm set progress=false && npm config set depth 0
# RUN npm install --only=production
# RUN cp -R node_modules prod_node_modules
RUN npm install

FROM dependencies AS test
COPY src/Settings.Web/ .
RUN  npm run lint

FROM base AS release
COPY --from=dependencies /app/node_modules ./node_modules
COPY . .
EXPOSE 4200
CMD npm run start
