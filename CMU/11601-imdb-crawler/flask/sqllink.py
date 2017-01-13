import MySQLdb
from sql_config import *

class DBConnector:
    conn = MySQLdb.connect(**config)
    cur = conn.cursor()

    def __init__(self):
        self.cur.execute("SELECT VERSION()")

    def createTables(self):
        self.cur.execute("Drop table if exists Movie");
        self.cur.execute("Drop table if exists Director");
        self.cur.execute("Drop table if exists Casts");
        self.cur.execute("Create table Movie (Name TEXT, Year YEAR, Rating DOUBLE, Dist INT)");
        self.cur.execute("Create table Director (Name TEXT, Movie TEXT)");
        self.cur.execute("Create table Casts (Name TEXT, Movie TEXT)");
        self.conn.commit()
    
    # Currently, record the name of the movie, the director and cast list, the year, country and rating.
    # Moreover, the bfs could also provide the distance of one movie to the keyword movie.
    def insert(self, Movie, Director, CastList, Year, Rating, Dist):
    	self.cur.execute("select * from Movie where Name = \"%s\"" % Movie +
    					" and Year = \"%d\"" % Year +
    					" and Rating = \"%f\"" % Rating)
    	for row in self.cur:
    		print "Exists!"
    		return False

        self.cur.execute("insert into Movie(Name, Year, Rating, Dist) values (\"%s\"" % Movie +
        				", \"%d\"" % Year +
        				", \"%f\"" % Rating +
        				", \"%d\")" % Dist)

        for direct in Director:
        	self.cur.execute("insert into Director(Name, Movie) values (\"%s\"" % direct +
        				", \"%s\")" % Movie)

        for Cast in CastList:
        	self.cur.execute("insert into Casts(Name, Movie) values (\"%s\"" % Cast +
        				", \"%s\")" % Movie)

        self.conn.commit()
        return True

    # returns the information of the movie
    def queryMovie(self, Movie):
        self.cur.execute("select * from Movie where Name = \"%s\"" % Movie)
        self.conn.commit()

        for row in self.cur:
            for element in row:
                print element,
            print

    # returns a list of movies of the cast
    def queryCast(self, Cast):
        self.cur.execute("select * from Cast where Name = \"%s\"" % Cast)
        self.conn.commit()

        for row in self.cur:
            for element in row:
                print element,
            print

    # returns a list of movies of the director
    def queryDirector(self, Director):
        self.cur.execute("select * from Director where Name = \"%s\"" % Cast)
        self.conn.commit()

        for row in self.cur:
            for element in row:
                print element,
            print

    # returns a list of suggested movies
    def makeSuggestion(self, Movie):
        self.cur.execute("select * from Movie where Dist = 1")
        self.conn.commit()

        for row in self.cur:
            for element in row:
                print element,
            print

"""
db = IMDBDatabase()
db.createTables()

Movie = "Showshank"
Year = 1994
Country = "USA"
Director = "Frank Darabont"
CastList = ["Tim Robbins", "Morgan Freeman", "Bob Gunton", "William Sadler"]
Dist = 0
Rating = 9.4

db.insert(Movie, Director, CastList, Year, Country, Rating, Dist)
db.queryMovie(Movie)
"""