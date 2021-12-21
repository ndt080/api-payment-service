package main

import (
	"fmt"
	"log"
	"net/http"
	"os"

	"github.com/gin-gonic/gin"

	_ "github.com/jackc/pgx/v4/stdlib"

	"github.com/jmoiron/sqlx"
)

type Service struct {
	ID   int    `db:"id"`
	Name string `db:"name"`
}

func checkService(c *gin.Context) {
	name_p := c.Query("name")

	db, err := sqlx.Connect("pgx", os.Getenv("DATABASE_URL"))

	if err != nil {
		log.Fatalln(err)
	}

	rows, err := db.Queryx("SELECT * FROM services")

	if err != nil {
		fmt.Println(err)
		return
	}

	service := Service{}

	for rows.Next() {
		err := rows.StructScan(&service)
		if err != nil {
			log.Fatalln(err)
		}
		if service.Name == name_p {
			c.JSON(http.StatusOK, gin.H{
				"result": true,
			})
			return
		}
	}

	c.JSON(http.StatusOK, gin.H{
		"result:": false,
	})
}

func main() {
	router := gin.Default()
	router.GET("/checkService", checkService)
	router.Run(":8080")
}
