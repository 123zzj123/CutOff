package services

import (
	"bytes"
	"crypto/md5"
	"fmt"
	"io"
	"math/rand"
	"net/http"
	"os"
	"strconv"
	"time"

	"github.com/HinanawiTenshi/myta_server/entities"
	"github.com/gorilla/mux"
	"github.com/unrolled/render"
)

func authHandler(formatter *render.Render) http.HandlerFunc {
	return func(w http.ResponseWriter, req *http.Request) {
		err:=req.ParseForm()
		if err != nil {
			panic(err)
		}
		user, has:=userDAO.FindBy("email", req.FormValue("email"))
		if !has {
			w.WriteHeader(http.StatusNotFound)
			return
		}
		pswHash := md5Hash(req.FormValue("password"))
		if user.Password != pswHash {
			w.WriteHeader(http.StatusBadRequest)
			return
		}
		user.Password = ""
		formatter.JSON(w, http.StatusOK, user)
	}
	// GET /v1/users{?api_key,course_id,type}
func usersGetHandler(formatter *render.Render) http.HandlerFunc {

	return func(w http.ResponseWriter, req *http.Request) {
		err := req.ParseForm()
		if err != nil {
			panic(err)
		}

		if !validateUser(req.FormValue("api_key")) {
			formatter.JSON(w, http.StatusForbidden, entities.Error{
				Error: "Permission denied"})
			return
		}

		userList := userDAO.FindAll()
		users := make([]entities.User, 0)
		if req.FormValue("course_id") != "" {
			courseID, _ := strconv.Atoi(req.FormValue("course_id"))
			if req.FormValue("type") != "" {
				for _, user := range userList {
					if user.Type != req.FormValue("type") {
						continue
					}
					for _, id := range user.Courses {
						if id == courseID {
							users = append(users, user)
							break
						}
					}
				}
			} else {
				for _, user := range userList {
					for _, id := range user.Courses {
						if id == courseID {
							users = append(users, user)
							break
						}
					}
				}
			}
		} else {
			if req.FormValue("type") != "" {
				for _, user := range userList {
					if user.Type == req.FormValue("type") {
						users = append(users, user)
					}
				}
			} else {
				users = userList
			}
		}
		formatter.JSON(w, http.StatusOK, users)
	}

}
}