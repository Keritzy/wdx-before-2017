from search_page import parseSearchPage
from movie_page import parseMoviePage
from cast_page import parseCastPage
from sqllink import DBConnector

import time

global SEED_URL
global HOST_URL
global db

db = DBConnector()

HOST_URL = "http://www.imdb.com"

def getMovieInfo(movie_link, dist):
	print movie_link
	info = parseMoviePage(movie_link)
	if info == None:
		return None
	succ = db.insert(info['title'], info['director'], info['cast'], \
			info['year'], info['rating'], dist)
	# Already Exists
	if not succ:
		return None
	return info

def dfsSearchCast(info, dist):
	# Set max depth as 10
	if dist >= 10:
		return

	# go through all the casts
	for cast_href in info['cast_href']:
		movies = parseCastPage(cast_href)
		if movies == None:
			continue
		# Search each movie of the cast
		for movie in movies['movie_href']:
			new_info = getMovieInfo(movie, dist + 1)
			# Not a movie page
			if new_info == None:
				continue
			# Continue searching
			dfsSearchCast(new_info, dist + 1)

def dfsSearchMovie(url, dist):
	# First parse the search page
	movie_href = parseSearchPage(url)

	for movie_link in movie_href:
		movie_link = HOST_URL + movie_link
		info = getMovieInfo(movie_link, dist)
		if info == None:
			continue
		dfsSearchCast(info, dist)

def bfsSearchMovie(url):
	href_queue = []
	head = 0

	movie_href = parseSearchPage(url)

	for movie_link in movie_href:
		movie_link = HOST_URL + movie_link
		href_queue.append({'href': movie_link, 'dist': 0})

	while (head <= len(href_queue)):
		cur = href_queue[head]
		info = getMovieInfo(cur['href'], cur['dist'])
		head += 1
		if info == None:
			continue

		for cast_href in info['cast_href']:
			movies = parseCastPage(cast_href)
			if movies == None:
				continue
			for movie in movies['movie_href']:
				href_queue.append({'href': movie, 'dist': cur['dist'] + 1})

		print href_queue


def main():
	SEED_URL = "http://www.imdb.com/find?q="
	TITLE_SEARCH = "&s=tt&ttype=ft&ref_=fn_tt_pop"
	#Popular 	#&ref_=fn_tt_pop"
	#Exact 		#&exact=true&ref_=fn_tt_ex"

	db.createTables()

	print "Please input the keyword:"
	keyword = raw_input()
	keyword.replace(" ", "+")
	SEED_URL = SEED_URL + keyword + TITLE_SEARCH
	#print SEED_URL

	#dfsSearchMovie(SEED_URL, 0)

	bfsSearchMovie(SEED_URL)

# main()
