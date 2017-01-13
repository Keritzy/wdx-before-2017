from HTMLParser import HTMLParser
from htmlentitydefs import name2codepoint
from sets import Set

class MyHTMLParser(HTMLParser):
    href_set =  []
    Name = ""
    Year = ""
    lasttag = ""

    def handle_starttag(self, tag, attrs):
        #print "Start tag:", tag
        for attr in attrs:
            #print "     attr:", attr
            if (attr[0] == "href"):
                #print "!!!", attr[1]
                if (not attr[1] in self.href_set):
                    self.href_set.append(attr[1])
            self.lasttag = attr[0]
            #print self.lasttag

    def handle_data(self, data):
        if (self.lasttag == "itemprop"):
            self.Name = data.lstrip()
            self.lasttag = ""
        if (self.lasttag == "href"):
            self.Year = data.lstrip()
            self.lasttag = ""
        #print "Data     :", data
    """
    def handle_endtag(self, tag):
        print "End tag  :", tag
    def handle_comment(self, data):
        print "Comment  :", data
    def handle_entityref(self, name):
        c = unichr(name2codepoint[name])
        print "Named ent:", c
    def handle_charref(self, name):
        if name.startswith('x'):
            c = unichr(int(name[1:], 16))
        else:
            c = unichr(int(name))
        print "Num ent  :", c
    def handle_decl(self, data):
        print "Decl     :", data
    """