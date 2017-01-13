function a = hardlim (n)
  if (isnumeric (n))
    a = double (n >= 0);
  elseif (isbool (n))
    a = ones (size (n));
  elseif (ischar (n))
    switch (tolower (n))
      case 'name',      a = 'Hard Limit Positive';
      case 'output',    a = [0 1];
      case 'fullderiv', a = 0;
      otherwise
        error ('hardlim: unknown command %s', n);
    end

  end
