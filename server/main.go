package main

import (
	"database/sql"
	"fmt"
	_ "github.com/go-sql-driver/mysql"
	// "bytes"
	"crypto/md5"
	"io"
	// "math/rand"
	"net/http"
	// "strconv"
	// "time"
	"github.com/mux"
	"github.com/negroni"
	"github.com/render"
)

// User Entity
type User struct {
	Username string
	Password string
	//Apikey      string `xorm:"'api_key' text" json:"api_key"`
}

func md5Hash(data string) string {
	hash := md5.New()
	io.WriteString(hash, data)
	return fmt.Sprintf("%x", hash.Sum(nil))
}

var admin User
var db *sql.DB

//main
func main() {
	admin.Username = "admin"
	admin.Password = "admin"
	db, _ = sql.Open("mysql", "root:1254860908@tcp(127.0.0.1:3306)/cut_off_user?charset=utf8")
	port := ":8080"
	server := NewServer()
	server.Run(port)
	fmt.Println("Server start successfully!")
}

//server
const Version string = "/v1/"

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
	url = Version + "auth"
	mx.HandleFunc(url, authHandler(formatter)).Methods("GET")
	//users
	url = Version + "users"
	mx.HandleFunc(url, usersPostHandler(formatter)).Methods("POST")
	mx.HandleFunc(url, usersGetHandler(formatter)).Methods("GET")
}

//handler
func authHandler(formatter *render.Render) http.HandlerFunc {
	return func(w http.ResponseWriter, req *http.Request) {
		err := req.ParseForm()
		if err != nil {
			panic(err)
		}
		var user User

		fmt.Println("username =", req.FormValue("username"))
		fmt.Println("password =", req.FormValue("password"))
		pswHash := md5Hash(req.FormValue("password"))
		err2 := db.QueryRow("select username, password from data where username=?", req.FormValue("username")).Scan(&user.Username, &user.Password)
		if err2 == sql.ErrNoRows {
			w.WriteHeader(http.StatusNotFound)
			return
		}
		if user.Password != pswHash {
			w.WriteHeader(http.StatusBadRequest)
			return
		}
		fmt.Println("Authorize successfully")
		formatter.JSON(w, http.StatusOK, user)
	}
}

// POST /v1/users
func usersPostHandler(formatter *render.Render) http.HandlerFunc {

	return func(w http.ResponseWriter, req *http.Request) {
		err := req.ParseForm()
		if err != nil {
			fmt.Println("POST parse form failed!")
			return
		}
		var user User
		username := req.FormValue("username")
		password := req.FormValue("password")
		fmt.Println("POST successfully!")
		fmt.Println(username, password)
		err2 := db.QueryRow("select id from data where username = ?", username).Scan(&user.Username, &user.Password)
		if err2 != sql.ErrNoRows {
			w.WriteHeader(http.StatusBadRequest)
			return
		}
		pswHash := md5Hash(req.FormValue("password"))
		user.Username = username
		user.Password = pswHash
		stmt, err2 := db.Prepare("insert data set username=?, password=?")
		if err2 != nil {
			fmt.Println(err2)
			panic(err2)
			return
		}
		res, err := stmt.Exec(username, pswHash)
		id, err := res.LastInsertId()
		fmt.Println("id =", id)
		formatter.JSON(w, http.StatusCreated, user)
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
