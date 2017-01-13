function W = randInitializeWeights(L_in, L_out)
epsilong = 0.12;
W = rand(L_out, 1 + L_in) * 2 * epsilong - epsilong;
end
