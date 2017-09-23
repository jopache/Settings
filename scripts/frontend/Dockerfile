FROM alpine:3.5 AS base
RUN apk add --no-cache nodejs-current tini nginx && rm -rf /var/cache/apk/*
WORKDIR /app
ENTRYPOINT ["/sbin/tini", "--"]
COPY src/Settings.Web/package.json .

FROM base AS dependencies
RUN npm set progress=false && npm config set depth 0
RUN npm install --only=production
RUN cp -R node_modules prod_node_modules
RUN npm install

FROM dependencies as source
COPY src/Settings.Web/ .

FROM source AS test
RUN  npm run lint

FROM source AS build
RUN npm run build

# FROM base AS release
# COPY --from=dependencies /app/prod_node_modules ./node_modules
# COPY --from=builder /app/dist ./

FROM base as release
EXPOSE 80
COPY --from=build /app/dist /usr/share/nginx/
RUN ls -al /usr/share/nginx/
CMD ["nginx", "-g", "daemon off;"]
