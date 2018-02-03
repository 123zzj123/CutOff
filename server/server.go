package services

import (
	"github.com/gorilla/mux"
	"github.com/unrolled/render"
	"github.com/urfave/negroni"
)

const Version string="/v1/"

func NewServer() *negroni.Negroni {
	formatter:=render.New(render.Options{IndentJSON:true})

	n:=negroni.Classic()
	mx:=mux.NewRouter()
	initRoutes(mx, formatter)
	n.UserHandler(mx)
	return n
}

func initRoutes(mx *mux.Router, formater *render.Render) {
	var url string
	//authorize
	url=Version + "auth"
	mx.HandleFunc(url, authHandler(formatter)).Methods("GET")
	//users
	url=Version + "users"
	mx.HandleFunc(url, usersPostHandler(formatter)).Methods("POST")
	mx.HandleFunc(url, usersGetHandler(formatter)).Methods("GET")
}