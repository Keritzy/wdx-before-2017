function [ls] = logSum(x)
    mx = max(x);
    x = x - mx;
    ls = mx + log(sum(exp(x)));
end
