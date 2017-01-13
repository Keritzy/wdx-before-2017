function retval = radbas (n)

  if (nargin ~= 1)
    print_usage ();
  else
    retval = exp (-n.^2);
  end
end


