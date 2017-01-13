package com.dawang;

import java.text.DecimalFormat;
import java.util.*;

/**
 * This is the class for finishing all the word analysis work
 * Created by dawang on 9/19/15.
 */
public class Analyser {
    // hashmap for the keywords with their density score
    private static HashMap<String, Double> keyWords =
            new HashMap<String, Double>();
    // sorted keywords list using the score as the keys so as to find the Top N
    // keywords
    static TreeMap<Double, ArrayDeque<String>> sortedKeyWords
            = new TreeMap<Double, ArrayDeque<String>>(Collections.reverseOrder());

    // the number of keywords that will be shown to the user
    private final static int KEYWORD_COUNT = 5;
    // the number of words to form a ngram phrase
    private final static int N_NGRAM = 3;
    // The threshold to select keywords to sort
    private final static double KEYWORD_THRESHOLD = 1.0;
    private final static boolean DEBUG_MODE = false;
    /**
     * Though the testcases given by the assignment contain some information
     * about the page, but for common occasion(more general search), the
     * information can be numbers or computer generated series. Thus I choose
     * not to consider the url for keywords selection.
     */
    private final static double[] WEIGHTS = {
            3.0, // 0: Title weight
            2.5, // 1: Metadata weight
            2.0, // 2: Header weight
            0.1, // 3: Body weight
            0.5, // 4: Href weight
    };

    /**
     * Find the most relevant topic and print the result
     *
     * @param url the url of the html page
     */
    public static void printResult(String url){
        HtmlPage html = new HtmlPage(url);

        HashMap<String, Double> ngramElements = null;
        System.out.println("Parsing URL: " + url);

        // load stop words
        Ngrammer.loadStopwords();

        // check content
        if (html.checkDocStatus()){
            System.out.println("Failed in fetching data. Please try again");
            return;
        }

        // get keywords and generate ngrams for them, then added to the keywords
        System.out.println("Parsing Title");
        ngramElements = Ngrammer.ngrams(html.getTitle(), N_NGRAM, WEIGHTS[0]);
        addToKeywords(ngramElements);
        if (DEBUG_MODE){
            System.out.println(ngramElements.toString());
        }


        System.out.println("Parsing Metadata");
        ngramElements = Ngrammer.ngrams(html.getMeta(), N_NGRAM, WEIGHTS[1]);
        addToKeywords(ngramElements);
        if (DEBUG_MODE){
            System.out.println(ngramElements.toString());
        }

        // only handle h1 - h4
        System.out.println("Parsing Headers");
        for (int i = 1; i <= 4; i++) {
            for (String header : html.getHeader(i)) {
                ngramElements = Ngrammer.ngrams(header, N_NGRAM,
                        WEIGHTS[2] * Math.pow(2, 1 - i));
                addToKeywords(ngramElements);
                if (DEBUG_MODE){
                    System.out.println(ngramElements.toString());
                    System.out.println();
                }
            }
        }

        System.out.println("Parsing Body");
        ngramElements = Ngrammer.ngrams(html.getBody(), N_NGRAM, WEIGHTS[3]);
        addToKeywords(ngramElements);
        if (DEBUG_MODE){
            System.out.println(ngramElements.toString());
            System.out.println();
        }

        System.out.println("Parsing Href");
        ngramElements = Ngrammer.ngrams(html.getHref(), N_NGRAM, WEIGHTS[4]);
        addToKeywords(ngramElements);
        if (DEBUG_MODE){
            System.out.println(ngramElements.toString());
            System.out.println();
        }


        addToSortedKeywords();

        printTopResult();

    }

    /**
     * Combine the current result into the keywords hashmap.
     * From Title, Meta, Header and Body
     *
     * @param addonHashMap the current hashmap from title/meta/header/body
     */
    private static void addToKeywords(HashMap<String, Double> addonHashMap){
        for (String key : addonHashMap.keySet()) {
            double value = 0.0;

            if (keyWords.containsKey(key))
                value = keyWords.get(key);

            keyWords.put(key, value + addonHashMap.get(key));
        }
    }

    /**
     * Add all the elements in keywords to sortedKeywords in order to sort each
     * of them with there score
     */
    private static void addToSortedKeywords(){
        for (String key: keyWords.keySet()){
            double value = keyWords.get(key);

            if (value > KEYWORD_THRESHOLD){
                ArrayDeque<String> strList = new ArrayDeque<String>();
                if (sortedKeyWords.containsKey(value))
                    strList = sortedKeyWords.get(value);

                strList.add(key);
                sortedKeyWords.put(value, strList);
            }

        }
    }

    private static void printTopResult(){
        DecimalFormat df = new DecimalFormat("#.000");

        int i = 1;
        System.out.println("\nRank\tScore\tKeywords");
        System.out.println("----\t-----\t--------");

        for (double element : sortedKeyWords.keySet()) {
            // break after nth keyword
            if (i > KEYWORD_COUNT)
                break;

            System.out.println(" " + i + "\t" + df.format(element) + "\t"
                    + printValues(sortedKeyWords.get(element)));
            i++;
        }

    }

    /**
     * Return the keywords with the same score in a unified string.
     *
     * @param keywords the array deque that stores the values of keywords
     * @return the keywords with the same score in a unified string.
     */
    private static String printValues(ArrayDeque<String> keywords) {

        StringBuilder valueBuilder = new StringBuilder();
        Iterator it = keywords.iterator();

        if (it.hasNext())
            valueBuilder.append(it.next());

        while (it.hasNext()) {
            valueBuilder.append(", ");
            valueBuilder.append(it.next());
        }

        return valueBuilder.toString();
    }
}
