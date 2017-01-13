function [ XFea ] = extractFea( XImg )

    % uncommnet foloowing to use hog feature
    [ XFea ] = hog_extractFea( XImg );

    
    % XFea = XImg;
end

