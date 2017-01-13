package com.dawang;

import java.io.*;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.HashSet;

/**
 * Generate Ngrams using the given input string.
 *
 * Ngram is a very common technique used in natural language processing to find
 * phrases. I built the n-grams based on the wiki page
 *
 * https://en.wikipedia.org/wiki/N-gram
 *
 * Created by dawang on 9/19/15.
 */
public class Ngrammer {
    // assume that each meaningful ngram has more than 3 letters.
    // Can remove spaces and small words which can be regarded as stopwords
    private static int MIN_NGRAM_LETTER_LENGTH = 3;
    private static HashSet<String> stopwords = new HashSet<String>();

    /**
     * Generate a hashmap with the ngrams of the input string as well as its
     * score(the higher the score, the more important keywords are)
     *
     * @param inputStr input string
     * @param n the n for n-grams
     * @param score the value to measure the relevance. Different parts of the
     *              hmlt page have the different weights.
     * @return
     */
    public static HashMap<String, Double> ngrams(String inputStr, int n,
                                                 double score){

        HashMap<String, Double> ngramElements = new HashMap<String, Double>();
        // get word list
        String[] words = inputStr.toLowerCase().split("[^a-z-0-9']");
        // remove the stopwords
        ArrayList<String> meaningWords = new ArrayList<String>();
        for (int i = 0; i < words.length; i++){
            if (!stopwords.contains(words[i])){
                meaningWords.add(words[i]);
            }
        }

        // only phrase with at least 2 words can be regarded as n-gram
        for (int j = 2; j <= n; j++){
            for (int i = 0; i < meaningWords.size() - j + 1; i++){
                StringBuilder phraseBuilder = new StringBuilder();

                for (int k = i; k < i + j; k++){
                    phraseBuilder.append(meaningWords.get(k) + " ");
                }

                // clear unnecessary whitespace
                String element =
                        phraseBuilder.toString().trim().replaceAll("( )+", " ");

                if (element.length() > MIN_NGRAM_LETTER_LENGTH){
                    double curScore = 0.0;
                    if (ngramElements.containsKey(element)){
                        curScore = ngramElements.get(element);
                    }
                    ngramElements.put(element, curScore + score);
                }
            }
        }
        return ngramElements;
    }

    public static void loadStopwords(){
        System.out.println("Loading Stopwords");
        BufferedReader br = null; // buffer reader
        try {
            // use this method for IDEA running
            br = new BufferedReader(new FileReader(new File("stopwords.txt")));

            // use this method for jar
            //InputStream is = Ngrammer.class.getResourceAsStream("/stopwords.txt");
            //br = new BufferedReader(new InputStreamReader(is));

            String word = br.readLine();
            while (word != null){
                stopwords.add(word);
                word = br.readLine();
            }
        } catch (FileNotFoundException fe) {
            // if file not found
            System.out.println("Error -- " + fe.toString());
        } catch (IOException e) {
            // catch IOException
            System.out.println("Error -- " + e.toString());
        } finally {
            try {
                if (br != null) {// if file not found, it will be null
                    br.close();
                    System.out.println("Loading Completed");
                }
            } catch (IOException brCloseException) {
                // catch IOExcetion for br.close()
                System.out.println("Error -- " + brCloseException.toString());
            }
        }
    }
}
