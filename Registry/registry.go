package main

import (
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"net/http"
	"os"

	"github.com/gin-gonic/gin"
	_ "github.com/jackc/pgx/v4/stdlib"
	"github.com/jmoiron/sqlx"
	"github.com/joho/godotenv"

	_ "github.com/pzmicer/registry/docs"
	swaggerFiles "github.com/swaggo/files"
	ginSwagger "github.com/swaggo/gin-swagger"
)

type Service struct {
	ID        int    `db:"id" json:"id,omitempty"`
	Name      string `db:"name" json:"name"`
	Cost      string `db:"cost" json:"cost"`
	Duration  int    `db:"duration" json:"duration"`
	Currency  string `db:"currency" json:"currency"`
	Url       string `db:"url" json:"url"`
	KeyMethod string `db:"key_method" json:"key_method"`
}

type ServiceNoID struct {
	Name      string `db:"name" json:"name"`
	Cost      string `db:"cost" json:"cost"`
	Duration  int    `db:"duration" json:"duration"`
	Currency  string `db:"currency" json:"currency"`
	Url       string `db:"url" json:"url"`
	KeyMethod string `db:"key_method" json:"key_method"`
}

type CheckResult struct {
	Result bool `json:"result"`
}

func getConnection() *sqlx.DB {
	db, err := sqlx.Connect("pgx", os.Getenv("DATABASE_URL"))
	if err != nil {
		log.Fatalln(err)
	}
	return db
}

// @Param name query string true "Service name"
// @Success 200 {object} CheckResult
// @Router /checkService [get]
func checkService(c *gin.Context) {
	serviceName := c.Query("name")

	db := getConnection()

	service := Service{}
	err := db.Get(&service, "SELECT * FROM services WHERE name = $1", serviceName)
	if err != nil {
		log.Println(err)
		c.JSON(http.StatusOK, gin.H{
			"result": false,
		})
		return
	}

	c.JSON(http.StatusOK, gin.H{
		"result": true,
	})
}

// @Param name query string true "Service name"
// @Success 200 {object} Service
// @Router /getServiceInfo [get]
func getServiceInfo(c *gin.Context) {
	serviceName := c.Query("name")

	db := getConnection()

	service := Service{}
	err := db.Get(&service, "SELECT * FROM services WHERE name = $1", serviceName)
	if err != nil {
		log.Println(err)
		c.JSON(http.StatusOK, gin.H{})
		return
	}

	c.JSON(http.StatusOK, service)
}

// @Success 200 {array} Service
// @Router /getServiceList [get]
func getServiceList(c *gin.Context) {
	db := getConnection()

	services := []Service{}
	err := db.Select(&services, "SELECT * FROM services")
	if err != nil {
		log.Fatalln(err)
	}
	c.JSON(http.StatusOK, services)
}

// @Param service body ServiceNoID  true "Body parameter"
// @Success 200
// @Router /addService [post]
func addService(c *gin.Context) {
	body, _ := ioutil.ReadAll(c.Request.Body)
	var newService ServiceNoID
	err := json.Unmarshal(body, &newService)
	if err != nil {
		log.Fatalln(err)
	}

	db := getConnection()

	db.NamedExec(`INSERT INTO services (name, cost, duration, currency, url, key_method) 
		VALUES (:name, :cost, :duration, :currency, :url, :key_method)`, newService)

	c.Status(http.StatusOK)
}

// @Param id query int  true "ID"
// @Success 200
// @Router /removeService [delete]
func removeService(c *gin.Context) {
	id := c.Query("id")

	db := getConnection()

	db.Exec("DELETE FROM services WHERE id = $1", id)

	c.Status(http.StatusOK)
}

func main() {
	godotenv.Load()
	router := gin.Default()
	router.GET("/checkService", checkService)
	router.GET("/getServiceInfo", getServiceInfo)
	router.GET("/getServiceList", getServiceList)
	router.POST("/addService", addService)
	router.DELETE("/removeService", removeService)
	router.GET("/swagger/*any", ginSwagger.WrapHandler(swaggerFiles.Handler))
	router.Run(fmt.Sprintf(":%s", os.Getenv("PORT")))
}
