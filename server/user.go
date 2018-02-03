package entities

// User Entity
type User struct {
	ID          int    `xorm:"'id' int pk autoincr" json:"id"`
	Username    string `xorm:"'username' text" json:"username"`
	Password    string `xorm:"'password' text" json:"password"`
	Email       string `xorm:"'email' text" json:"email"`
	Apikey      string `xorm:"'api_key' text" json:"api_key"`
}
