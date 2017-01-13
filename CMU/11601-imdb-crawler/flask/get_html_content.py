from bs4 import BeautifulSoup
import urllib2

def getHTMLContent(url):
	soup = BeautifulSoup(urllib2.urlopen(url).read(), "html.parser")
	#print soup.prettify()
	return soup.prettify()