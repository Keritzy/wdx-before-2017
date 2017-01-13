from get_html_content import getHTMLContent
from parser import MyHTMLParser
from get_html_range import getHTMLRange

def parseSearchPage(url):
	page_content = getHTMLContent(url)
	split_page_content = page_content.split("\n")

	parser = MyHTMLParser()
	#parser.feed(page_content)

	#Get the movie list content
	movie_list = getHTMLRange(split_page_content, "<table class=\"findList\">", "</div>")\

	for movies in movie_list:
		parser.feed(movies)

	#print len(parser.href_set)
	return parser.href_set