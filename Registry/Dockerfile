# syntax=docker/dockerfile:1

FROM golang:1.17.4-alpine3.15

WORKDIR /app

COPY go.mod ./
COPY go.sum ./
RUN go mod download

COPY *.go ./

COPY docs/* ./docs/

RUN go build -o /registry-server


FROM alpine

WORKDIR /

COPY --from=0 /registry-server /registry-server

EXPOSE ${PORT}

ENTRYPOINT [ "/registry-server" ]