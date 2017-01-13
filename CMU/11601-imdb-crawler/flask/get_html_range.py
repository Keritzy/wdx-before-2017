def getHTMLRange(contents, start, end):
	ret_list = []
	flag = False
	for content in contents:
		if (start in content):
			flag = True
		if (flag):
			if (end in content):
				break
			ret_list.append(content)

	return ret_list