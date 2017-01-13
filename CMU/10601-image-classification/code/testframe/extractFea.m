function [ XFea ] = extractFea( XImg)

    % uncommnet foloowing to use hog feature
    load('fea/hog/fea_filt.mat')
    [ XFea ] = hog_extractFea( XImg );
    XFea = XFea(:,feaind);
    
    
    % newXFea = XFea(:,3:end) .* XFea(:,1:end-2) .* XFea(:,2:end-1);
    % XFea = [XFea,newXFea];
    
    
    % XFea = double(XImg);
    
    
    % XFea = normalize(XFea);
end


function normX = normalize(X) 
meanX = mean(X,2);
stdX  = std(X,0,2);
normX = bsxfun(@minus,X,meanX);
normX = bsxfun(@rdivide,normX,stdX);
end
