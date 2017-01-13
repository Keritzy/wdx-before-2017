package com.dawang;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;

import java.util.HashMap;

/**
 * @author Da Wang
 * This is the main function for the word density analysis.
 */

public class Main {
    /**
     * Main method for word density analysis
     *
     * @param args The URL of the websites that should be handle
     */
    public static void main(String[] args) {
        // check if the number of arguments is correct
        if (args.length != 1){
            System.out.println("Usage: java -jar wordensity.jar <arg>");
            System.out.println("<arg> is a URL");
            return;
        }

        String url = null;
        try {
            url = args[0];
        }
        catch (Exception e){
            System.out.println("Something wrong with the input URL");
            e.printStackTrace();
        }
        finally {
            Analyser.printResult(url);
        }
    }
}
