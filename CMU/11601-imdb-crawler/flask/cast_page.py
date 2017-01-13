from bs4 import BeautifulSoup
import urllib2

HOST_URL = "http://www.imdb.com"

def parseCastPage(url):
	try:
	    soup = BeautifulSoup(urllib2.urlopen(url).read(), "html.parser")
	    films = soup.find("div", "filmo-category-section").findAll("b")
	    movie = []
	    movie_href = []
	    for film in films:
	        movie += [film.a.string.strip()]
	        movie_href += [HOST_URL + film.a["href"]]
	    dict = {"movie":movie, "movie_href":movie_href}
	    return dict

	except:
		#print "Not Cast!"
		return None