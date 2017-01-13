function [p] = sigmoidProb(y, x, w)
% p : n*1, output probability
% y : n*1, y should be either 0 or 1
% x : n * f
% w : (f+1) * 1
% n = size(y,1);
% f = size(x,2);
% x = [ones(n,1),x];
inner_prod = x * w;
p0 = 1./(1 + exp(inner_prod));
p1 = 1 - p0;
p = (y==0) .* p0 + (y==1) .* p1;
        
end
