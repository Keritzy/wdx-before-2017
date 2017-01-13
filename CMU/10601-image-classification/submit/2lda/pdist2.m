function [D] = pdist2(X,Y)
    m = size(X,1); 
    n = size(Y,1);
    D = sum(X.^2,2) * ones(1,n) + ones(m,1) * sum(Y.^2,2)' - 2 .* X * Y';
    D = sqrt(D);
end

