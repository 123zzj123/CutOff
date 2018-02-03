package services

import (
	"strconv"
	"entities"
)

type UserDBServer struct{}

var userDAO = UserDBServer{}

// InsertOne inserts a user into database
func (*UserDBServer) InsertOne(u *entities.User) {
	_, err := engine.Table(userTable).InsertOne(u)
	if err != nil {
		panic(err)
	}
}

// UpdateByID updates a user according to ID
func (*UserDBServer) UpdateByID(id int, u *entities.User) {
	_, err := engine.Table(userTable).ID(id).Update(u)
	if err != nil {
		panic(err)
	}
}

// FindByID queries a user according to ID
func (*UserDBServer) FindByID(id int) (entities.User, bool) {
	var u entities.User
	has, err := engine.Table(userTable).ID(id).Get(&u)
	if err != nil {
		panic(err)
	}
	return u, has
}

// FindBy queries a user according to a column
func (*UserDBServer) FindBy(colName, value string) (entities.User, bool) {
	var u entities.User
	has, err := engine.Table(userTable).Where(colName+"=?", value).Get(&u)
	if err != nil {
		panic(err)
	}
	return u, has
}

// FindIDBy queries an ID according to a condition
func (*UserDBServer) FindIDBy(colName, value string) (int, bool) {
	var id int
	has, err := engine.Table(userTable).Where(colName+"=?", value).Get(&id)
	if err != nil {
		panic(err)
	}
	return id, has
}

// FindAll queries all users from the database
func (*UserDBServer) FindAll() []entities.User {
	l := make([]entities.User, 0)
	err := engine.Table(userTable).Find(&l)
	if err != nil {
		panic(err)
	}
	return l
}