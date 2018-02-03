package main

import (
	"services"
)

func main() {
	port:=":8080"
	server:=services.NewServer()
	server.Run(port)
}