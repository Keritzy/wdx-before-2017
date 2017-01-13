package com.dawang;

import org.jsoup.Connection;
import org.jsoup.HttpStatusException;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import java.util.ArrayList;
import java.util.Iterator;

/**
 * This is the class for getting the specific html page from the given url and
 * parse it with basic operations. Using the jsoup module to handle the parsing
 * work
 *
 * Created by dawang on 9/19/15.
 */
public class HtmlPage {
    private String url;
    private Document doc = null;

    /**
     * Constructor of the class, handle basic exceptions
     *
     * @param url
     * The url of the page that needs to be parsed
     */
    public HtmlPage(String url){
        this.url = url;
        try {
            System.out.println("Fetching Content from: " + url);
            // set a longer timeout to handle slow internet connection
            Connection conn = Jsoup.connect(url);
            conn.header("User-Agent", "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/29.0.1547.57 Safari/537.36");
            doc = Jsoup.connect(url).timeout(7000).get();

        }
        catch (Exception e){
            System.out.println("Invalid URL or NO Internet connection.");
            e.printStackTrace();
        }
    }

    /**
     * Get the title of the html page
     *
     * @return the title of the html page
     */
    public String getTitle(){
        return doc.title();
    }

    /**
     * Get the metadata of the html page
     *
     * @return the metadata html page
     */
    public String getMeta(){
        StringBuilder meta = new StringBuilder();

        for (Element ele : doc.select("meta[name=description]")) {
            meta.append(" ");
            meta.append(ele.attr("content"));
        }

        return meta.toString();
    }

    /**
     * Get the header of the html page, e.g. H1, H2, H3, ....
     *
     * @param headerSize The number followed with letter 'H' e.g. 1, 2, 3, ...
     */
    public ArrayList<String> getHeader(int headerSize){
        ArrayList<String> headerList = new ArrayList<String>();

        for (Element ele : doc.select("h" + headerSize))
            headerList.add(ele.text());

        return headerList;
    }

    /**
     * Get the body of the html page
     *
     * @return the body of the html page
     */
    public String getBody(){
        return doc.body().text();
    }

    /**
     * Get the Href text of the html page
     *
     * @return the Href text of the html page
     */
    public String getHref(){
        Elements elements =  doc.select("a");
        StringBuilder href = new StringBuilder();
        for (Iterator it = elements.iterator(); it.hasNext();){
            Element e = (Element) it.next();
            href.append(" ");
            href.append(e.text());
        }

        return href.toString();
    }

    /**
     * Check if successfully fetches data from the html page
     *
     * @return true if doc != null, false otherwise
     */
    public boolean checkDocStatus(){
        return doc == null;
    }
}
