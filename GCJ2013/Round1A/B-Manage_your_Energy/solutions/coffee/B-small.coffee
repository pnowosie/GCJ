fs = require('fs')
_u = require('underscore')
lines = fs.readFileSync('../../tests/B-small-practice.in').toString().split('\n')

lines.reverse()

findAns = (e, val) ->
  #console.log "val length #{val.length}"
  e = Math.min (parseInt e) + (parseInt R), parseInt E
  #console.log "e: #{e}"
  
  if val.length == 0 then return 0
  if val.length == 1 then return e * val[0]
  
  f = _u.first val
  #console.log "f: #{f}"
  rest = _u.rest val
  #console.log rest
  
  max = 0
  for i in [0..e]
    v = i * f + (findAns e-i, rest)
    max = v if v > max
  max



T = lines.pop()
for C in [1..T]
  best = 0
  [ E, R, N ] = lines.pop().split(' ')
  values = lines.pop().split(' ')
  
  best = findAns E, values
  
  console.log "Case ##{C}: #{best}"

