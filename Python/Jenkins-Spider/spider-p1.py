# coding=utf-8

import urllib2, urllib, cookielib
import re, os
import gzip, StringIO, zlib

def inred(s):
    return "%s[31;2m%s%s[0m"%(chr(27), s, chr(27));

def ingreen(s):
    return "%s[32;2m%s%s[0m"%(chr(27), s, chr(27));

def inyellow(s):
    return "%s[33;2m%s%s[0m"%(chr(27), s, chr(27));

def inblue(s):
    return "%s[34;2m%s%s[0m"%(chr(27), s, chr(27));

def inpurple(s):
    return "%s[35;2m%s%s[0m"%(chr(27), s, chr(27));


def check(team):
    if team.find('15') > 0 or team.find('16') > 0 or team.find('005') > 0 or team.find('22') > 0 or team.find('33') > 0:
        return 1
    else :
        return 0


_GAP = '--------------------------------------------------------------------'
OFFLINE = 1

print _GAP
print '# Setting Basic Environment'
print _GAP

BASE = 'http://fast645.info:8080/'
LOGIN = '/login?from=/'
CHECK = 'j_acegi_security_check'
ALLKMEANS = 'job/all_kmeans/'
ALLMATMUL = 'job/all_matrix_mul/'
SUFFIX = '/lastBuild/consoleText'
PREFIX = 'job/'

# 'http://fast645.info:8080/job/team000_kmeans/lastBuild/console'
# 'http://fast645.info:8080/job/team000_matmul/lastBuild/console'

DATADIR = './data-p1/'
ALLMATMULFILE = 'allmatmul.txt'
ALLKMEANSFILE = 'allkmeans.txt'
FILESUFFIX = '.txt'

print 'BASE: ' + BASE
print 'LOGIN: ' + LOGIN
print 'CHECK: ' + CHECK
print 'ALLKMEANS: ' + ALLKMEANS
print 'ALLMATMUL: ' + ALLMATMUL

POSTDATA = urllib.urlencode({
    'j_username' : 'student',
    'j_password' : '18645',
    'from' : '/',
    'json' : '{"j_username": "student", "j_password": "18645", "remember_me": false, "from": "/"}',
    'Submit' : 'log in'
    })
print 'POSTDATA OK'

cookie= cookielib.CookieJar()
opener = urllib2.build_opener(urllib2.HTTPCookieProcessor(cookie))
print 'Cookie Support OK'


if os.path.exists(DATADIR):
    print 'Data Folder Exists'
    OFFLINE = 1
else:
    print 'Create Data Folder'
    os.mkdir(DATADIR)
    OFFLINE = 0
    print 'OK'


print _GAP
print '# Get Cookie to Log in'
print _GAP

reqlogin = urllib2.Request(
    url = BASE + CHECK,
    data = POSTDATA
)
print 'Login Request OK'

if OFFLINE != 1:
    print 'Try to Login...'
    loginresponse = opener.open(reqlogin)
    loginpage = loginresponse.read()
    index = loginpage.find('all_kmeans')
else:
    index = 1

# Login Success
if index != -1:
    print 'OK'

    if OFFLINE != 1:
        print 'Get Cookie Info...'
        cookiestr = ''
        for item in cookie:
            if item.name == 'JSESSIONID':
                cookiestr = item.name + '=' + item.value
        print cookiestr

        print 'Building HTTP Header...'
        headers = {
            'Host': 'fast645.info:8080' ,
            'Connection': 'keep-alive' ,
            'Content-Length': '195' ,
            'Cache-Control': 'max-age=0' ,
            'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8' ,
            'Origin': 'http://fast645.info:8080' ,
            'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/40.0.2214.94 Safari/537.36' ,
            'Content-Type': 'application/x-www-form-urlencoded' ,
            'Referer': 'http://fast645.info:8080/login?from=/' ,
            'Accept-Encoding': 'gzip, deflate' ,
            'Accept-Language': 'zh-CN,zh;q=0.8,en-US;q=0.6,en;q=0.4,zh-TW;q=0.2' ,
            'Cookie': cookiestr
        }
        print 'OK'

        reqallmatmul = urllib2.Request(
            url = BASE + ALLMATMUL,
            headers = headers
        )
        print 'Allmatmul Request OK'

        reqallkmeans = urllib2.Request(
            url = BASE + ALLKMEANS,
            headers = headers
        )
        print 'Allkmeans Request OK'

    print _GAP
    print '# Mission Start!'
    print _GAP

    print 'Try to Get All_Matmul List...'
    allmatmul = ''
    if os.path.exists(DATADIR+ALLMATMULFILE):
        print ALLMATMULFILE + ' Exists. Read it.'
        ammfile = open(DATADIR + ALLMATMULFILE, "r")
        allmatmul = ammfile.read()
    else:
        print ALLMATMULFILE + ' Do not Exist. Create it.'
        allmatmul = urllib2.urlopen(reqallmatmul).read()
        ammfile = open(DATADIR + ALLMATMULFILE, "w")
        print >> ammfile, allmatmul
    print 'OK'

    print 'Try to Get All_Kmenas List...'
    allkmeans = ''
    if os.path.exists(DATADIR+ALLKMEANSFILE):
        print ALLKMEANSFILE + ' Exists. Read it.'
        aksfile = open(DATADIR+ALLKMEANSFILE, "r")
        allkmeans = aksfile.read()
    else:
        print ALLKMEANSFILE + ' Do not Exist. Create it.'
        allkmeans = urllib2.urlopen(reqallkmeans).read()
        aksfile = open(DATADIR+ALLKMEANSFILE, "w")
        print >> aksfile, allkmeans
    print 'OK'


    print 'Find All Matmul Teams...'
    allteammatmul = re.findall( r'>team\d\d\d_matmul', allmatmul, re.I)
    if allteammatmul:
        print 'Total ' + str(len(allteammatmul)) + ' Matmul Teams'
        for team in allteammatmul:
            team = team[1:]
    else:
        print "No Result!!"

    print 'Find All Kmeans Teams...'
    allteamkmeans = re.findall( r'>team\d\d\d_kmeans', allkmeans, re.I)
    if allteamkmeans:
        print 'Total ' + str(len(allteamkmeans)) + ' Kmeans Teams'
    else:
        print "No Result!!"

    print _GAP
    print '# Getting ALL Matmul Console info'
    print _GAP

    MatmulResult = []
    KmeansResult = []

    for team in allteammatmul:
        print 'Handling ' + team[1:] + '...'
        content = ''
        if os.path.exists(DATADIR+team[1:]+FILESUFFIX):
            print team[1:] + FILESUFFIX + ' exists. Read it.'
            mm = open(DATADIR+team[1:]+FILESUFFIX, 'r')
            content = mm.read()
        else:
            print team[1:] + FILESUFFIX + ' do not exists. Get it.'
            reqmm = urllib2.Request(
                url = BASE + PREFIX + team[1:] + SUFFIX,
                headers = headers
            )
            print team[1:] + ' request OK'
            print 'Get console output...'
            content = urllib2.urlopen(reqmm).read()
            mm = open(DATADIR+team[1:]+FILESUFFIX, "w")
            content = zlib.decompress(content, 16+zlib.MAX_WBITS)
            print >> mm, content
            print 'OK'
        # create a tuple
        # Test Case 5.\d*.\d* millise
        flag = content.find('SUCCESS')
        score = ''

        if flag > 0:
            index = content.find('*OMP*')
            start = content.find('Test Case 5', index)
            end = content.find('milli', start)
            score = content[start+12:end-1]
            if score.find('Dimension') < 0 :
                mmt = (team[1:], 'SUCCESS', score)
                MatmulResult.append(mmt)
        #else:
            #mmt = (team[1:], 'FAILURE', 'NA')
            #MatmulResult.append(mmt)


    print _GAP
    print '# Getting ALL Kmeans Console info'
    print _GAP
    for team in allteamkmeans:
        print 'Handling ' + team[1:] + '...'
        content = ''
        if os.path.exists(DATADIR+team[1:]+FILESUFFIX):
            print team[1:] + FILESUFFIX + ' exists. Read it.'
            mm = open(DATADIR+team[1:]+FILESUFFIX, 'r')
            content = mm.read()
            # print content
        else:
            print team[1:] + FILESUFFIX + ' do not exists. Get it.'
            reqmm = urllib2.Request(
                url = BASE + PREFIX + team[1:] + SUFFIX,
                headers = headers
            )
            print team[1:] + ' request OK'
            print 'Get console output...'
            content = urllib2.urlopen(reqmm).read()
            mm = open(DATADIR+team[1:]+FILESUFFIX, "w")
            content = zlib.decompress(content, 16+zlib.MAX_WBITS)
            print >> mm, content
            print 'OK'

        flag = content.find('SUCCESS')
        score = ''
        if flag > 0:
            index = content.find('Input file:     /afs/andrew.cmu.edu/usr12/jchong/18645_spring_2015/codes/' + team[1:-7] + '/fastcode/kmeans/kmeans04.dat')
            start = content.find('Computation timing', index)
            end = content.find('sec', start)
            score = content[start+25:end-1]
            mmt = (team[1:], 'SUCCESS', score)
            KmeansResult.append(mmt)
        #else:
            #mmt = (team[1:], 'FAILURE', 'NA')
            #KmeansResult.append(mmt)

    print _GAP
    print '# Gradebook'
    print _GAP


    MatmulResult.sort(key=lambda l:(float(l[2])))
    print _GAP
    print 'Matmul'
    print _GAP
    i = 1
    for result in MatmulResult:
        if i < 10:
            if i == 1 or i == 2:
                print ingreen('0' + str(i) + ': ' + str(result))
            else:
                if check(result[0]) == 1:
                    print inpurple('0' + str(i) + ': ' + str(result))
                else:
                    print '0' + str(i) + ': ' + str(result)
        else:
            if check(result[0]) == 1:
                print inpurple(str(i) + ': ' + str(result))
            else:
                print str(i) + ': ' + str(result)
        i = i + 1

    KmeansResult.sort(key=lambda l:(float(l[2])))
    print _GAP
    print 'Kmeans'
    print _GAP
    i = 1
    length = len(KmeansResult)
    for result in KmeansResult:
        if i < 10:
            if i == 1 or i == 2:
                print ingreen('0' + str(i) + ': ' + str(result))
            else:
                if check(result[0]) == 1:
                    print inpurple('0' + str(i) + ': ' + str(result))
                else:
                    print '0' + str(i) + ': ' + str(result)
        else:
            if i == length or i == length - 1:
                print inred(str(i) + ': ' + str(result))
            else:
                if check(result[0]) == 1:
                    print inpurple(str(i) + ': ' + str(result))
                else:
                    print str(i) + ': ' + str(result)
        i = i + 1

    #print MatmulResult
    # handle 'content' and get the info

    #print content





else :
    print 'Login Failed'


print _GAP
print 'ALL DONE'





#

#print allkmeans.find('id="loadAllBuildHistory"')

#print allkmeans
#



# find '>teamxxx_kmeans' -> get 'teamxxx_kmeans' -> url BASE+'/job/teamxxx_kmeans/'
# -> pic '16/xxx.png' -> alt='xxx'

#result = urllib2.urlopen(req15).read()
#print result;

