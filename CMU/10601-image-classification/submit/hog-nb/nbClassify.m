function [t] = nbClassify(nbModel,xTest)
% xTest : n * f , n samples, with `f' features for each sample
% t     : n * 1 , predicted class for each sample

% nbModel Prameters : 
% M     : f * c , Mean, each feature each class
% V     : f * c , Variance, each feature each class
% p     : c * 1 , prior probability fo class P(class=i)

M = nbModel.M;
V = nbModel.V;
p = nbModel.p;

[n,f] = size(xTest);
c = size(p,1);

t = zeros(n,1);

for si = 1 : n % for each sample
    ci = 1;    
    max_c = 1;
    max_lpro = lgau(xTest(si,:),M(:,ci),V(:,ci));
    
    for ci = 2 : c
        lprob = lgau(xTest(si,:),M(:,ci),V(:,ci));
        if(lprob > max_lpro)
            max_lpro = lprob;
            max_c = ci;
        end
    end
    t(si) = max_c;
end

t = uint8(t - 1);
end

function lp = lgau(sample,m,v)


f = size(m,1);
%{
for gi = 1 : f
    lp = lp + log(normpdf(sample(gi),m(gi),(v(gi))));
end
%}
lp = sum(log(normpdf(sample',m,v)));
% lp = log(mvnpdf(sample',m,v'));

end