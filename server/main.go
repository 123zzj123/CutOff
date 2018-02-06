package main

import (
	"fmt"
	// "bytes"
	// "crypto/md5"
	// "fmt"
	// "io"
	// "math/rand"
	"net/http"
	// "os"
	// "strconv"
	// "time"
	"github.com/mux"
	"github.com/render"
	"github.com/negroni"
)

// User Entity
type User struct {
	ID          int    `xorm:"'id' int pk autoincr" json:"id"`
	Username    string `xorm:"'username' text" json:"username"`
	Password    string `xorm:"'password' text" json:"password"`
	Email       string `xorm:"'email' text" json:"email"`
	Apikey      string `xorm:"'api_key' text" json:"api_key"`
}

//main
func main() {
	port:=":8080"
	server:=NewServer()
	server.Run(port)
	fmt.Println("Server start successfully!")
}

//server
const Version string="/v1/"

func NewServer() *negroni.Negroni {
	formatter := render.New(render.Options{IndentJSON: true})

	n := negroni.Classic()
	mx := mux.NewRouter()

	initRoutes(mx, formatter)

	n.UseHandler(mx)

	return n
}

func initRoutes(mx *mux.Router, formatter *render.Render) {
	var url string
	//authorize
	url=Version + "auth"
	mx.HandleFunc(url, authHandler(formatter)).Methods("GET")
	//users
	url=Version + "users"
	mx.HandleFunc(url, usersPostHandler(formatter)).Methods("POST")
	mx.HandleFunc(url, usersGetHandler(formatter)).Methods("GET")
}

//handler
func authHandler(formatter *render.Render) http.HandlerFunc {
	return func(w http.ResponseWriter, req *http.Request) {
		err:=req.ParseForm()
		if err != nil {
			panic(err)
		}
		fmt.Println("Authorize successfully")
		// user, has:=userDAO.FindBy("email", req.FormValue("email"))
		// if !has {
		// 	w.WriteHeader(http.StatusNotFound)
		// 	return
		// }
		// pswHash := md5Hash(req.FormValue("password"))
		// if user.Password != pswHash {
		// 	w.WriteHeader(http.StatusBadRequest)
		// 	return
		// }
		// user.Password = ""
		// formatter.JSON(w, http.StatusOK, user)
	}
}

	// POST /v1/users
func usersPostHandler(formatter *render.Render) http.HandlerFunc {

	return func(w http.ResponseWriter, req *http.Request) {
		err := req.ParseForm()
		if err != nil {   
			panic(err)
		}

		username := req.FormValue("username")
		email := req.FormValue("email")
		fmt.Println("POST successfully!")
		fmt.Println(email,username)
		// _, hasUsername := userDAO.FindBy("username", username)
		// _, hasEmail := userDAO.FindBy("email", email)
		// if hasUsername || hasEmail {
		// 	w.WriteHeader(http.StatusBadRequest)
		// 	return
		// }

		// rndKey := generateKey()
		// pswHash := md5Hash(req.FormValue("password"))
		// user := entities.User{
		// 	Username:    username,
		// 	Password:    pswHash,
		// 	Email:       email,
		// 	Apikey:      rndKey}
		// userDAO.InsertOne(&user)
		// user.Password = ""
		// formatter.JSON(w, http.StatusCreated, user)
	}

}
	// GET /v1/users{?api_key,course_id,type}
func usersGetHandler(formatter *render.Render) http.HandlerFunc {

	return func(w http.ResponseWriter, req *http.Request) {
		err := req.ParseForm()
		if err != nil {
			panic(err)
		}
		fmt.Println("GET successfully")
		// if !validateUser(req.FormValue("api_key")) {
		// 	formatter.JSON(w, http.StatusForbidden, entities.Error{
		// 		Error: "Permission denied"})
		// 	return
		// }

		// userList := userDAO.FindAll()
		// users := make([]entities.User, 0)
		// if req.FormValue("course_id") != "" {
		// 	courseID, _ := strconv.Atoi(req.FormValue("course_id"))
		// 	if req.FormValue("type") != "" {
		// 		for _, user := range userList {
		// 			if user.Type != req.FormValue("type") {
		// 				continue
		// 			}
		// 			for _, id := range user.Courses {
		// 				if id == courseID {
		// 					users = append(users, user)
		// 					break
		// 				}
		// 			}
		// 		}
		// 	} else {
		// 		for _, user := range userList {
		// 			for _, id := range user.Courses {
		// 				if id == courseID {
		// 					users = append(users, user)
		// 					break
		// 				}
		// 			}
		// 		}
		// 	}
		// } else {
		// 	if req.FormValue("type") != "" {
		// 		for _, user := range userList {
		// 			if user.Type == req.FormValue("type") {
		// 				users = append(users, user)
		// 			}
		// 		}
		// 	} else {
		// 		users = userList
		// 	}
		// }
		// formatter.JSON(w, http.StatusOK, users)
	}

}