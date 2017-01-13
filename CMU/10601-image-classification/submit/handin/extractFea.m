function [ XFea ] = extractFea( XImg )

    % uncommnet foloowing to use hog feature
    [ XFea ] = hog_extractFea( XImg );
    load('fea_filt.mat');
    XFea = XFea(:,feaind);
    
    % XFea = double(XImg);
end

