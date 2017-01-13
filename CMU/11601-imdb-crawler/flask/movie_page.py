from bs4 import BeautifulSoup
import urllib2

import time

HOST_URL = "http://www.imdb.com"

def parseMoviePage(url):
	try:
		soup = BeautifulSoup(urllib2.urlopen(url).read(), "html.parser")
		title = soup.find(id="overview-top").find(itemprop="name").string.strip()
		year = int(soup.find("span", "nobr").a.string.strip())
		details = soup.find(id="titleDetails").findAll("div", "txt-block")
		"""
		countries = details[1].findAll(itemprop="url")
		country = []
		for c in countries:
			country += [c.string.strip()]
		"""
		rating = float(soup.find(itemprop="ratingValue").string.strip())
		casts = soup.find(itemprop="actors")
		cast = []
		for c in casts.findAll(itemprop="name"):
			cast += [c.string.strip()]
		cast_href = []
		for c in casts.findAll(itemprop="url"):
			cast_href += [HOST_URL + c['href']]
		directors = soup.find(itemprop="director").findAll(itemprop="name")
		director = []
		for d in directors:
			director += [d.string.strip()]
		dict = {"title":title, "year":year, "rating":rating, \
				"cast":cast, "cast_href":cast_href, "director":director}
		print dict
		return dict
	except:
		print "Not Movie!"
		return None
